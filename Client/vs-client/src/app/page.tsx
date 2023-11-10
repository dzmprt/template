"use client";

import Image from "next/image";
import styles from "./page.module.css";
import Card from "@/components/Elements/Card";
import NameLabel from "@/components/Elements/NameLabel";
import TicketInput from "@/components/Elements/TicketInput";
import EnterTicketBlock from "@/components/Blocks/EnterTicketBlock";
import useSWR, { Fetcher } from "swr";
import IContestInfo from "@/models/IContestInfo";

import { useSearchParams } from "next/navigation";
import { useEffect, useState } from "react";
import axios, { AxiosError, HttpStatusCode } from "axios";
import IVoteSet from "@/models/IVoteSet";
import CategoryBlock from "@/components/Blocks/CategoryBlock";
import IParticipantInfo from "@/models/IParticipantInfo";

export default function Home() {
  const [error, setError] = useState<string>();

  const [contest, setContest] = useState<IContestInfo>();

  const [loading, setLoading] = useState<boolean>(true);

  const [voteSet, setVoteSet] = useState<IVoteSet>();

  const [saved, setSaved] = useState<boolean>(true);

  const searchParams = useSearchParams();

  const contestId = searchParams.get("contestId");

  useEffect(() => {
    loadContest();
  }, [null]);

  function loadContest() {
    if (contestId == null) {
      setLoading(false);
      setError("В URL не указан Id для голосования");
    } else {
      axios
        .get(
          process.env.NEXT_PUBLIC_CLIENT_SERVER_HOST +
            "api/Contests/" +
            contestId
        )
        .then((res) => {
          console.log(res);
          var data = res.data as IContestInfo;
          if (data.contestCategories == undefined) {
            setError("Ошибка загрузки голосования");
          } else {
            setContest(data);
          }
          setLoading(false);
          console.log(data);
        })
        .catch((error: AxiosError) => {
          if (error.response?.status == HttpStatusCode.NotFound) {
            setError("Неверный адрес голосования");
          } else {
            setError("Ошибка загрузки голосования");
          }
          setLoading(false);
        });
    }
  }

  function onLoadVoteSet(value: IVoteSet) {
    setVoteSet(value);
  }

  function onSelectParticipant(value: IParticipantInfo) {
    var participantsFromCategory = contest?.contestCategories.find((c) =>
      c.participants.find((p) => p.id == value.id)
    )?.participants;

    voteSet!.votes = voteSet!.votes.filter(
      (v) => !participantsFromCategory?.find((c) => c.id == v.participantId)
    )!;

    const newSet = {
      ticketKey: voteSet?.ticketKey,
      votes: voteSet!.votes,
      contestId: voteSet?.contestId,
    } as IVoteSet;
    newSet.votes.push({ participantId: value.id, prizeNumber: 1 });
    setVoteSet(newSet);
  }

  function onCancelSelectParticipant(value: IParticipantInfo) {
    var participantsFromCategory = contest?.contestCategories.find((c) =>
      c.participants.find((p) => p.id == value.id)
    )?.participants;

    voteSet!.votes = voteSet!.votes.filter(
      (v) => !participantsFromCategory?.find((c) => c.id == v.participantId)
    )!;

    const newSet = {
      ticketKey: voteSet?.ticketKey,
      votes: voteSet!.votes,
      contestId: voteSet?.contestId,
    } as IVoteSet;
    setVoteSet(newSet);
  }

  function save() {
    setLoading(true);
    axios
      .post(
        process.env.NEXT_PUBLIC_CLIENT_SERVER_HOST + "api/VoteSets",
        voteSet
      )
      .then((res) => {
        setVoteSet(undefined);
        loadContest();
        setLoading(false);
        alert("Выбор сохранен");
      })
      .catch((error: AxiosError) => {
        if (error.response?.status == HttpStatusCode.BadRequest) {
          alert("Голосование уже закончилось");
        } else {
          alert("Ошибка: " + error.response?.statusText);
        }
        setLoading(false);
      });
  }

  return (
    <main>
      <>
        <NameLabel />
        {loading && (
          <Card className="text-center">
            <h1>Loading...</h1>
          </Card>
        )}
        {!loading && (
          <>
            {!voteSet && (
              <>
                {!contest?.finished && contest?.started && (
                  <EnterTicketBlock
                    contestId={contest.id}
                    onLoadVoteSet={onLoadVoteSet}
                  />
                )}
                {contest?.finished && (
                  <Card className="text-center">
                    <h1>Голосование уже закончилось</h1>
                  </Card>
                )}
                {!contest?.started && !error && (
                  <Card className="text-center">
                    <h1>Голосование еще не началось</h1>
                  </Card>
                )}
                {error && (
                  <Card className="text-center">
                    <h1>{error}</h1>
                  </Card>
                )}
              </>
            )}
            {voteSet &&
              contest?.contestCategories.map((c) => (
                <CategoryBlock
                  onCancelSelectParticipant={onCancelSelectParticipant}
                  onSelectParticipant={onSelectParticipant}
                  key={c.id}
                  voteSet={voteSet}
                  category={c}
                />
              ))}
            {voteSet && (
              <div className={styles.resultButtonContainer}>
                <button className={styles.resultButton} onClick={save}>
                  Отправить результаты
                </button>
              </div>
            )}
          </>
        )}
      </>
    </main>
  );
}
