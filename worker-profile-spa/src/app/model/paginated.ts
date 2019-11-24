export class Paginated<T> {
  constructor(
    public page: number,
    public pageCount: number,
    public pageSize: number,
    public totalElements: number,
    public payload: T
  ) {}
}
