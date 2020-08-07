import { Owner } from "../owners/owner";
import { Device } from "../sensors/device";
import { Attributes } from "react";

export interface Contract {
        id?: number,
        owner?: Owner,
        ownerId?: number,
        blackAndWhiteLimitSet: number,
        colorLimitSet: number,
        blackAndWhitePrice: number,
        colorPrice: number,
        surplusBlackAndWhitePrice: number,
        surplusColorPrice: number,
        contractDevices?: ContractDevice[],
        contractUsers?: ContractUser[],
        contractEmployees?: ContractEmployee[],
}

export interface ContractDevice {
        objId: string,
        device: Device,
        contractId: number,
        contract: Contract
}
export interface ContractUser {
        user: CognitoUser,
        userId: string,
        contractId: number,
        contract: Contract
}
export interface ContractEmployee {
        employeeId : number,
        employee: Person,
        contractId: number,
        contract: Contract
}
export interface CognitoUser {
        userId: string,
        userName?: string,
        attributes: CognitoUserAttributes,
}
export interface CognitoUserAttributes {
        email: string,
        email_verified?: boolean,
        phone_number_verified?: boolean,
        phone_number?: string,
}
export interface Person {
        id?: number,
        owner?: Owner,
        name?: string,
        email: string,
        contractEmployees?: ContractEmployee[]
}
