"use client";

import Card from "@/components/Elements/Card";
import NameLabel from "@/components/Elements/NameLabel";
import TicketInput from "@/components/Elements/TicketInput";
import styles from "./index.module.css";
import { useState } from "react";
import IVoteSet from "@/models/IVoteSet";
import axios, { AxiosError, HttpStatusCode } from "axios";

export interface IInputData {
  onLoadVoteSet: (value: IVoteSet) => void;
  contestId: number;
}

export default function EnterTicketBlock(props: IInputData) {
  const [ticket, setTicket] = useState("");

  const [error, setError] = useState<string>();

  function onChangeTicket(value: string) {
    setTicket(value);
  }

  function onGetVoteSet() {
    axios
      .get(
        process.env.NEXT_PUBLIC_CLIENT_SERVER_HOST + "api/VoteSets/" + props.contestId + "/" + ticket
      )
      .then((res) => {
        if (res.status === HttpStatusCode.Ok) {
          props.onLoadVoteSet(res.data);
        }
      })
      .catch((error: AxiosError) => {
        if (error.response?.status == HttpStatusCode.NotFound) {
          setError("Билет не найден");
        }
      });
  }

  return (
    <>
      <Card className={"text-center " + styles.card}>
        <h3>Введите код для голосования</h3>
        <TicketInput onChangeValue={onChangeTicket} />
        <br />
        <button
          disabled={ticket.length != 6}
          onClick={onGetVoteSet}
          className={
            styles.btn + (ticket.length === 6 ? "" : " " + styles.btnDisable)
          }
        >
          Отправить
        </button>
        <>{error && <div className={styles.error}>{error}</div>}</>
      </Card>
    </>
  );
}
