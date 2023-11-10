import styles from "./index.module.css";

export default function NameLabel() {
  return (
    <div className="text-center text-white">
          <h1 className={styles.f40}>WE17</h1>
      <h3>
       Голосование ПЗС
      </h3>
      <h4>Вы можете менять свой выбор неограниченное кол-во раз пока голосование не будет закрыто</h4>
    </div>
  );
}
