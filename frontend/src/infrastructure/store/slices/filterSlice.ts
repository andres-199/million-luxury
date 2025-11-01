import { createSlice, type PayloadAction } from "@reduxjs/toolkit";

export interface FilterState {
  searchText: string;
  minPrice: number;
  maxPrice: number;
}

export const MAX_PRICE = 10000000;
export const MIN_PRICE = 0;

const initialState: FilterState = {
  searchText: "",
  minPrice: MIN_PRICE,
  maxPrice: MAX_PRICE,
};

const filterSlice = createSlice({
  name: "filter",
  initialState,
  reducers: {
    setSearchText: (state, action: PayloadAction<string>) => {
      state.searchText = action.payload;
    },
    setPriceRange: (
      state,
      action: PayloadAction<{ min: number; max: number }>
    ) => {
      state.minPrice = action.payload.min;
      state.maxPrice = action.payload.max;
    },
    resetFilters: () => initialState,
  },
});

export const { setSearchText, setPriceRange, resetFilters } =
  filterSlice.actions;
export default filterSlice.reducer;
