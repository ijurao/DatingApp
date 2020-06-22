export interface IPagination {

    currentPage: number;
    count: number;
    totalItems: number;
    totalPages: number;
}

export class PaginatedResult<T> {
   result: T;
   pagination: IPagination;
}
