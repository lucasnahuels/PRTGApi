import { Owner } from "../owners/owner";
import { Device} from "../sensors/device";

export interface Contract {
        id? : number,
        owner?: Owner,
        ownerId?: number,
        device? :Device,
        deviceId: number,
        blackAndWhiteLimitSet:number,
        colorLimitSet:number,
        blackAndWhitePrice :number,
        colorPrice  :number,
        surplusBlackAndWhitePrice :number,
        surplusColorPrice: number, 
        users?: CognitoUser[], 
        usersid?: User[], 
        employees?: Person[], 
}

export interface CognitoUser {
        userId?: Number,
}

export interface User {
        userId?: Number,
}

export interface Person {
        id?: number,
        company? : Owner,
        name?: string,
        email: string,
}
