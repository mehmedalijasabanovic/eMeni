import {BasePagedQuery} from '../../core/models/paging/base-paged-query';
import {PageResult} from '../../core/models/paging/page-result';

export class ListOnlyMenusRequest extends BasePagedQuery{
  categoryId?: number;
  city?: string;
}

export interface ListOnlyMenusDto {
  id: number;
  menuDescription?: string;
  menuTitle?: string;
  promotionRank?:number;

}

export type ListOnlyMenusResponse=PageResult<ListOnlyMenusDto>
