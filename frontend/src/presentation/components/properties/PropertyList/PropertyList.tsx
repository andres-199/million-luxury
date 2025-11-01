import type { Property } from "../../../../domain/entities";
import { usePropertyList } from "./usePropertyList";
import { PropertyListView } from "./PropertyListView";

interface PropertyListProps {
  properties: Property[];
  isLoading: boolean;
  isError: boolean;
  error?: unknown;
}

export const PropertyList = ({
  properties,
  isLoading,
  isError,
  error,
}: PropertyListProps) => {
  const {
    selectedProperty,
    dialogOpen,
    handlePropertyClick,
    handleDialogClose,
  } = usePropertyList();

  return (
    <PropertyListView
      properties={properties}
      isLoading={isLoading}
      isError={isError}
      error={error}
      selectedProperty={selectedProperty}
      dialogOpen={dialogOpen}
      handlePropertyClick={handlePropertyClick}
      handleDialogClose={handleDialogClose}
    />
  );
};
