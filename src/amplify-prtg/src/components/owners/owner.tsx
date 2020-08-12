import { Employee } from "../contracts/contract";

export interface Owner {
  id?: number;
  name?: string;
  employees?: Employee[];
}
