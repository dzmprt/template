import styles from './index.module.css';

export interface Props {
    children: JSX.Element|JSX.Element[];
    className?: string;
}

export default function Card(props: Props) {
  const className = [styles.card, props.className].join(" ");

  return <div className={className}>{props.children}</div>;
}