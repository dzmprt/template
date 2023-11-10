import IVote from "./IVote";

export default interface IVoteSet{
    ticketKey: string;
    votes: IVote[];
    contestId: number;
}