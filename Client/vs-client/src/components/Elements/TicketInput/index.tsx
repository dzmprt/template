"use client";

import { useEffect, useState } from "react";
import styles from "./index.module.css";

export interface IInputData {
  onChangeValue: (value: string) => void;
}

export default function TicketInput(props: IInputData) {
  const [value, setValue] = useState("");

  function onChangeField(e: React.ChangeEvent<HTMLInputElement>) {
    var v = e.target.value.toUpperCase().trim();
    if (v.length < 7) {
      setValue(v);
      props.onChangeValue(v);
    }
  }

  return (
    <>
      <input
        type="text"
        placeholder="Введите код"
        value={value}
        className={styles.input}
        onChange={onChangeField}
      />
    </>
  );
}
