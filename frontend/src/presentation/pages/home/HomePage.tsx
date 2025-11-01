import { Box, Typography, Stack } from "@mui/material";
import { PropertyFilters, PropertyList } from "../../components/properties";
import { useGetPropertiesQuery } from "../../../infrastructure/api";
import {
  MAX_PRICE,
  MIN_PRICE,
  useAppSelector,
} from "../../../infrastructure/store";
import { useTranslation } from "react-i18next";

export const HomePage = () => {
  const { t } = useTranslation();
  const { searchText, minPrice, maxPrice } = useAppSelector(
    (state) => state.filter
  );

  const {
    data: properties = [],
    isLoading,
    isError,
    error,
  } = useGetPropertiesQuery({
    name: searchText || undefined,
    address: searchText || undefined,
    minPrice: minPrice > MIN_PRICE ? minPrice : undefined,
    maxPrice: maxPrice < MAX_PRICE ? maxPrice : undefined,
  });

  return (
    <Box sx={{ width: "100%", maxWidth: "100%", overflow: "hidden" }}>
      <Stack
        direction={{ xs: "column", lg: "row" }}
        spacing={3}
        alignItems={{ xs: "flex-start", lg: "center" }}
        sx={{ mb: 3, width: "100%" }}
      >
        <Typography
          variant="h4"
          component="h1"
          fontWeight="bold"
          sx={{
            minWidth: "fit-content",
            background: (theme) =>
              `linear-gradient(135deg, ${theme.palette.primary.main} 0%, ${theme.palette.primary.dark} 100%)`,
            WebkitBackgroundClip: "text",
            WebkitTextFillColor: "transparent",
            backgroundClip: "text",
          }}
        >
          {t("properties.title")}
        </Typography>

        <Box sx={{ flexGrow: 1, width: "100%" }}>
          <PropertyFilters />
        </Box>
      </Stack>

      <Box sx={{ width: "100%", overflow: "hidden" }}>
        <PropertyList
          properties={properties}
          isLoading={isLoading}
          isError={isError}
          error={error}
        />
      </Box>
    </Box>
  );
};
