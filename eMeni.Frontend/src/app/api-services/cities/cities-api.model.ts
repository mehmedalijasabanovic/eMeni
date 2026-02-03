import {BasePagedQuery} from '../../core/models/paging/base-paged-query';
import {PageResult} from '../../core/models/paging/page-result';

export class ListCitiesRequest extends BasePagedQuery{
  search?: string;
}

export interface ListCitiesDto {
  id: number;
  name: string;

}

export type ListCitiesResponse=PageResult<ListCitiesDto>
