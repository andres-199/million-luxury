import { usePropertyFilters } from "./usePropertyFilters";
import { PropertyFiltersView } from "./PropertyFiltersView";

export const PropertyFilters = () => {
  const {
    localSearchText,
    setLocalSearchText,
    priceRange,
    handlePriceChange,
    handlePriceCommit,
    handleClearSearch,
  } = usePropertyFilters();

  return (
    <PropertyFiltersView
      localSearchText={localSearchText}
      setLocalSearchText={setLocalSearchText}
      priceRange={priceRange}
      handlePriceChange={handlePriceChange}
      handlePriceCommit={handlePriceCommit}
      handleClearSearch={handleClearSearch}
    />
  );
};
