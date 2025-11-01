import { useState, useEffect } from "react";
import {
  useAppDispatch,
  useAppSelector,
  setSearchText,
  setPriceRange,
} from "../../../../infrastructure/store";

export const usePropertyFilters = () => {
  const dispatch = useAppDispatch();
  const { searchText, minPrice, maxPrice } = useAppSelector(
    (state) => state.filter
  );

  const [localSearchText, setLocalSearchText] = useState(searchText);
  const [priceRange, setPriceRangeLocal] = useState<number[]>([
    minPrice,
    maxPrice,
  ]);

  useEffect(() => {
    const timer = setTimeout(() => {
      dispatch(setSearchText(localSearchText));
    }, 500);

    return () => clearTimeout(timer);
  }, [localSearchText, dispatch]);

  const handlePriceChange = (_event: Event, newValue: number | number[]) => {
    setPriceRangeLocal(newValue as number[]);
  };

  const handlePriceCommit = (
    _event: Event | React.SyntheticEvent,
    newValue: number | number[]
  ) => {
    const [min, max] = newValue as number[];
    dispatch(setPriceRange({ min, max }));
  };

  const handleClearSearch = () => {
    setLocalSearchText("");
  };

  return {
    localSearchText,
    setLocalSearchText,
    priceRange,
    handlePriceChange,
    handlePriceCommit,
    handleClearSearch,
  };
};
