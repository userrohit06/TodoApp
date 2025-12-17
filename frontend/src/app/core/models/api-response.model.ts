export interface ApiRespone<T> {
  status: number;
  message: string;
  data: T;
}
