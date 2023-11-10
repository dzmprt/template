import styles from "./index.module.css";
import IParticipantInfo from "@/models/IParticipantInfo";

export interface IProps {
  participant: IParticipantInfo;
  onClose: () => void;
  onSelect: (value: IParticipantInfo) => void;
}

export default function ParticipantDetailsBlock(props: IProps) {
  const handlers = {
    onClose: () => {
      props.onClose();
    },
    onSelect: () => {
      props.onSelect(props.participant);
    },
  };

  return (
    <div>
      <div className={styles.container}>
        {props.participant.images.map((i) => {
          const srcLite = `${process.env.NEXT_PUBLIC_CLIENT_SERVER_HOST}api/Images/Lite/${i.id}_lite.jpeg`;
          const srcFull = `${process.env.NEXT_PUBLIC_CLIENT_SERVER_HOST}api/Images/${i.id}.jpeg`;
          return (
            <div className={styles.imgContainer}>
              <a title="Открыть полное изображение" target="_blank"  href={srcFull}>
                <div className={styles.fullImageBtn}>
                  <img
                    className={styles.magnifyingGlassIcon}
                    src="/magnifying-glass-solid.svg"
                  />
                </div>
              </a>
              <img
                className={styles.img}
                src={srcLite}
                alt={props.participant.name + " img"}
              />
            </div>
          );
        })}
        {props.participant.description && (
          <div className={styles.description}>
            {props.participant.description}
          </div>
        )}
        <br />
        <button
          className={`${styles.btn} ${styles.btnRead}`}
          onClick={handlers.onSelect}
        >
          Выбрать
        </button>
        <button onClick={handlers.onClose} className={styles.btn}>
          Свернуть
        </button>
      </div>
    </div>
  );
}
