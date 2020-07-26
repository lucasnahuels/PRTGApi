import { Person } from "../contracts/contract";

export interface Company {
  name: string;
  employees?: Person[];
}
