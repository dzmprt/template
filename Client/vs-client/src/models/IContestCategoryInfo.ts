import IParticipantInfo from "./IParticipantInfo";

export default interface IContestCategoryInfo{
    id: number;
    name: string;
    participants: IParticipantInfo[];
}