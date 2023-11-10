// import Axios, { AxiosResponse, Canceler } from "axios";

// const CancelGetWorkingHoursToken = Axios.CancelToken;

// let cancelGetWorkingHours: Canceler;

// export class InfoService {
//     public getInfo(): Promise<AxiosResponse<IApplicationInfo>> {
//         return Axios.get<IApplicationInfo>(AppConfig.serverUrl + "api/Info");
//     }

//     public getWorkingHours(year: number): Promise<AxiosResponse<IWorkingHours[]>> {
//         if (cancelGetWorkingHours !== undefined) {
//             cancelGetWorkingHours();
//         }
//         return Axios.get<IWorkingHours[]>(AppConfig.serverUrl + "api/Info/WorkingHours", {
//             params: { year },
//             cancelToken: new CancelGetWorkingHoursToken(function executor(c: any) {
//                 cancelGetWorkingHours = c;
//             }),
//         });
//     }
// }

// export const infoService = new InfoService();