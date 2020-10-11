import { Owner } from "../owners/owner";
import { Device } from "../sensors/device";

export interface Contract {
        id?: number,
        owner?: Owner,
        ownerId?: number,
        blackAndWhiteLimitSet?: number,
        colorLimitSet?: number,
        blackAndWhitePrice?: number,
        colorPrice?: number,
        surplusBlackAndWhitePrice?: number,
        surplusColorPrice?: number,
        contractDevices?: ContractDevice[],
        contractUsers?: ContractUser[],
        contractEmployees?: ContractEmployee[],
}

export interface ContractDevice {
        objId?: string,
        device?: Device,
        contractId?: number,
        contract?: Contract
}
export interface ContractUser {
        user?: CognitoUser,
        userId?: string,
        contractId?: number,
        contract?: Contract
}
export interface ContractEmployee {
        employeeId? : number,
        employee?: Employee,
        contractId?: number,
        contract?: Contract
}
export interface CognitoUser {
        userID?: string,
        userName?: string,
        attributes: CognitoUserAttributes,
        sendReport?: boolean
}
export interface CognitoUserAttributes {
        email: string,
        email_verified?: boolean,
        phone_number_verified?: boolean,
        phone_number?: string,
}
export interface Employee {
        id?: number,
        owner?: Owner,
        ownerId?: number,
        name?: string,
        email: string,
        contractEmployees?: ContractEmployee[],
        sendReport? : boolean
}
