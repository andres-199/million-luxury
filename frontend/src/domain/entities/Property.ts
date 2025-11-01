import type { Owner } from "./Owner";
import type { PropertyImage } from "./PropertyImage";
import type { PropertyTrace } from "./PropertyTrace";

export interface Property {
  id: string;
  name: string;
  address: string;
  price: number;
  codeInternal: string;
  year: number;
  ownerId: string;
  owner?: Owner;
  images: PropertyImage[];
  traces: PropertyTrace[];
  createdAt: string;
  updatedAt?: string;
}
