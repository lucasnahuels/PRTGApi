import { Owner } from "../owners/owner";
import { Device} from "../sensors/device";
import { Attributes } from "react";

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
        userId: string,
        userName?: string,
        attributes: CognitoUserAttributes,
        sendReport: boolean
}
export interface CognitoUserAttributes {
        email: string,
        email_verified?: boolean,
        phone_number_verified?: boolean,
        phone_number?: string,
}

export interface User {
        userId?: Number,
}

export interface Person {
        id?: number,
        company? : Owner,
        name?: string,
        email: string,
        sendReport: boolean
}
