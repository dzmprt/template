import { IParticipantImageInfo } from "./IParticipantImageInfo";

export default interface IParticipantInfo{
    id: number;
    name: string;
    description: string;
    images: IParticipantImageInfo[];
}