import { useState } from "react";
import type { Property } from "../../../../domain/entities";

export const usePropertyList = () => {
  const [selectedProperty, setSelectedProperty] = useState<Property | null>(
    null
  );
  const [dialogOpen, setDialogOpen] = useState(false);

  const handlePropertyClick = (property: Property) => {
    setSelectedProperty(property);
    setDialogOpen(true);
  };

  const handleDialogClose = () => {
    setDialogOpen(false);
  };

  return {
    selectedProperty,
    dialogOpen,
    handlePropertyClick,
    handleDialogClose,
  };
};
