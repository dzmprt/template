import IContestCategoryInfo from "./IContestCategoryInfo";

export default interface IContestInfo{
    id: number;
    name: string;
    started: boolean;
    finished: boolean;
    maximumNumberOfVotesInCategory: number;
    contestCategories: IContestCategoryInfo[];
}