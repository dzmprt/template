import { useState } from "react";
import styles from "./index.module.css";
import IParticipantInfo from "@/models/IParticipantInfo";
import Image from "next/image";
import Card from "@/components/Elements/Card";
import ParticipantDetailsBlock from "../ParticipantDetailsBlock";

export interface IProps {
  participant: IParticipantInfo;
  selected: boolean;
  onSelectParticipant: (value: IParticipantInfo) => void;
  onCancelSelectParticipant: (value: IParticipantInfo) => void;
}

export default function ParticipantBlock(props: IProps) {
  const [open, setOpen] = useState(false);

  const handler = {
    onClose: () => {
      document
        .getElementById(`participant-block-${props.participant.id}`)!
        .scrollIntoView();
      setOpen(false);
    },
    onOpen: () => {
      setOpen(true);
    },
    onSelect: () => {
      props.onSelectParticipant(props.participant);
      handler.onClose();
    },
    cancelSelect: () => {
      props.onCancelSelectParticipant(props.participant);
      handler.onClose();
    },
  };

  return (
    <div className={styles.container} id={`participant-block-${props.participant.id}`}>
      <div
        className={
          styles.participant + (props.selected ? " " + styles.selected : "")
        }
      >
        <div>{props.participant.name}</div>
        <div className={styles.buttons}>
          {!props.selected && (
            <button
              onClick={handler.onSelect}
              className={`${styles.btnSelect} ${styles.btn} ${styles.btnGreen}`}
            >
              Выбрать
            </button>
          )}
          {props.selected && (
            <button
              onClick={handler.cancelSelect}
              className={`${styles.btnSelect} ${styles.btn} ${styles.btnRead}`}
            >
              Отменить
            </button>
          )}
          {!open && (
            <button onClick={handler.onOpen} className={`${styles.btn}`}>
              Подробнее <i className={styles.rightArrow}></i>
            </button>
          )}
          {open && (
            <button onClick={handler.onClose} className={`${styles.btn}`}>
              Подробнее
              <i className={styles.downArrow}></i>
            </button>
          )}
        </div>
      </div>
      {open && (
        <ParticipantDetailsBlock
          participant={props.participant}
          onSelect={handler.onSelect}
          onClose={handler.onClose}
        />
      )}
      <div></div>
    </div>
  );
}
