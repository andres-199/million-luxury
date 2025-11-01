import { Box, Typography, Paper } from "@mui/material";
import { SearchOff } from "@mui/icons-material";
import type { Property } from "../../../../domain/entities";
import { PropertyCard } from "../PropertyCard";
import { PropertyCardSkeleton } from "../PropertyCardSkeleton";
import { PropertyDetailDialog } from "../PropertyDetailDialog";
import { ErrorMessage } from "../../common";
import { useTranslation } from "react-i18next";

interface PropertyListViewProps {
  properties: Property[];
  isLoading: boolean;
  isError: boolean;
  error?: unknown;
  selectedProperty: Property | null;
  dialogOpen: boolean;
  handlePropertyClick: (property: Property) => void;
  handleDialogClose: () => void;
}

export const PropertyListView = ({
  properties,
  isLoading,
  isError,
  error,
  selectedProperty,
  dialogOpen,
  handlePropertyClick,
  handleDialogClose,
}: PropertyListViewProps) => {
  const { t } = useTranslation();

  if (isError) {
    const errorMessage =
      error && typeof error === "object" && "message" in error
        ? String(error.message)
        : "Failed to load properties";

    return <ErrorMessage message={errorMessage} />;
  }

  if (isLoading) {
    return (
      <Box
        sx={{
          display: "grid",
          gridTemplateColumns: {
            xs: "minmax(0, 1fr)",
            md: "repeat(2, minmax(0, 1fr))",
          },
          gap: 3,
          width: "100%",
        }}
      >
        {Array.from({ length: 6 }).map((_, index) => (
          <PropertyCardSkeleton key={index} />
        ))}
      </Box>
    );
  }

  if (!properties || properties.length === 0) {
    return (
      <Box
        display="flex"
        flexDirection="column"
        alignItems="center"
        justifyContent="center"
        minHeight="500px"
        py={8}
      >
        <Paper
          elevation={0}
          sx={{
            p: 6,
            textAlign: "center",
            backgroundColor: "background.default",
            borderRadius: 2,
          }}
        >
          <SearchOff
            sx={{
              fontSize: 80,
              color: "text.secondary",
              opacity: 0.5,
              mb: 2,
            }}
          />
          <Typography
            variant="h5"
            color="text.secondary"
            gutterBottom
            fontWeight={600}
          >
            {t("properties.noResults.title")}
          </Typography>
          <Typography
            variant="body1"
            color="text.secondary"
            sx={{ mt: 1, maxWidth: 400 }}
          >
            {t("properties.noResults.description")}
          </Typography>
        </Paper>
      </Box>
    );
  }

  return (
    <Box sx={{ width: "100%", overflow: "hidden" }}>
      <Box
        sx={{
          display: "grid",
          gridTemplateColumns: {
            xs: "1fr",
            md: "repeat(2, 1fr)",
          },
          gap: 3,
          width: "100%",
          "& > *": {
            minWidth: 0,
            width: "100%",
          },
        }}
      >
        {properties.map((property, i) => (
          <Box key={property.id + i} sx={{ width: "100%", minWidth: 0 }}>
            <PropertyCard
              property={property}
              onClick={() => handlePropertyClick(property)}
            />
          </Box>
        ))}
      </Box>

      <PropertyDetailDialog
        property={selectedProperty}
        open={dialogOpen}
        onClose={handleDialogClose}
      />
    </Box>
  );
};
