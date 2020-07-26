import { Company } from "../owners/owner";

export interface Contract {
        id? : number,
        owner :Company,
        device :string,
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
        company : Company,
        name: string,
        email: string,
}
