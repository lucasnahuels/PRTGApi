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
        employees?: Employee[], 
}

export interface CognitoUser {
        userId?: Number,
}

export interface User {
        userId?: Number,
}

export interface Employee {
        company : Company,
        name: string,
        email: string,
}

export interface Company {
        name: string,
        employees?: Employee[], 
}