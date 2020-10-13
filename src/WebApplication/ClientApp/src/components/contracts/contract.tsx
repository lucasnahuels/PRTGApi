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
        user?: User,
        user_Id?: string,
        contractId?: number,
        contract?: Contract
}
export interface ContractEmployee {
        employeeId? : number,
        employee?: Employee,
        contractId?: number,
        contract?: Contract
}
export interface Auth0User {
        user_Id?: string,
        nickName?: string,
        email?: string,
        sendReport?: boolean
}
export interface User {
        user_Id?: string
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
