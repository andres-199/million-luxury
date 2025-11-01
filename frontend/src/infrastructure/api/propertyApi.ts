import { baseApi } from "./baseApi";
import { API_ENDPOINTS } from "./config";
import type { Property } from "../../domain/entities";
import type { GetPropertiesParams } from "../../application/dto";

export const propertyApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    getProperties: builder.query<Property[], GetPropertiesParams | void>({
      query: (params) => {
        const queryParams = new URLSearchParams();

        if (params) {
          if (params.name) queryParams.append("name", params.name);
          if (params.address) queryParams.append("address", params.address);
          if (params.minPrice !== undefined)
            queryParams.append("minPrice", params.minPrice.toString());
          if (params.maxPrice !== undefined)
            queryParams.append("maxPrice", params.maxPrice.toString());
          if (params.page) queryParams.append("page", params.page.toString());
          if (params.pageSize)
            queryParams.append("pageSize", params.pageSize.toString());
        }

        const queryString = queryParams.toString();
        return `${API_ENDPOINTS.PROPERTIES}${
          queryString ? `?${queryString}` : ""
        }`;
      },
      providesTags: (result) =>
        result
          ? [
              ...result.map(({ id }) => ({ type: "Property" as const, id })),
              { type: "Property", id: "LIST" },
            ]
          : [{ type: "Property", id: "LIST" }],
    }),
    getPropertyById: builder.query<Property, string>({
      query: (id) => `${API_ENDPOINTS.PROPERTIES}/${id}`,
      providesTags: (_result, _error, id) => [{ type: "Property", id }],
    }),
  }),
});

export const { useGetPropertiesQuery, useGetPropertyByIdQuery } = propertyApi;
