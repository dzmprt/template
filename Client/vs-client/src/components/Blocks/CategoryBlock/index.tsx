import IContestCategoryInfo from "@/models/IContestCategoryInfo";
import { useState } from "react";
import styles from "./index.module.css";
import ParticipantBlock from "../ParticipantBlock";
import IVoteSet from "@/models/IVoteSet";
import IParticipantInfo from "@/models/IParticipantInfo";

export interface IProps {
  category: IContestCategoryInfo;
  voteSet: IVoteSet;
  onSelectParticipant: (value: IParticipantInfo) => void;
  onCancelSelectParticipant: (value: IParticipantInfo) => void;
}

export default function CategoryBlock(props: IProps) {
  const [open, setOpen] = useState(false);

  const selectedParticipant = getCategoryParticipant();

  function getCategoryParticipant(): IParticipantInfo | undefined {
    var categoryVote = props.voteSet.votes.filter((v) =>
      props.category.participants.find((p) => p.id == v.participantId)
    );
    if (categoryVote.length > 0) {
      const result = props.category.participants.find(
        (p) => p.id == categoryVote[0].participantId
      );
      console.log(result);
      return result;
    }
    return undefined;
  }

  const handlers = {
    onOpen: () => {
      setOpen(true);
    },
    onClose: () => {
      setOpen(false);
    },
  };

  return (
    <div className={styles.container}>
      <div className={styles.category}>
        <div>
          {selectedParticipant && <span className={styles.done}>✓</span>}{" "}
          {props.category.name}{" "}
          {selectedParticipant && " - " + selectedParticipant?.name}
        </div>
        <div>
          {!open && (
            <button onClick={handlers.onOpen} className={styles.openButton}>
              <i className={styles.rightArrow}></i>
            </button>
          )}
          {open && (
            <button onClick={handlers.onClose} className={styles.openButton}>
              <i className={styles.downArrow}></i>
            </button>
          )}
        </div>
      </div>
      {open &&
        props.category.participants.map((p) => (
          <ParticipantBlock
            key={p.id}
            onSelectParticipant={props.onSelectParticipant}
            onCancelSelectParticipant={props.onCancelSelectParticipant}
            selected={p.id == selectedParticipant?.id}
            participant={p}
          />
        ))}
    </div>
  );
}
