import {BasePagedQuery} from '../../core/models/paging/base-paged-query';
import {PageResult} from '../../core/models/paging/page-result';

export class ListBusinessCategoriesRequest extends BasePagedQuery{
  search?: string;
}

export interface ListBusinessCategoriesDto {
  id: number;
  categoryName: string;
  categoryDescription: string;
}

export type ListBusinessCategoriesResponse=PageResult<ListBusinessCategoriesDto>
