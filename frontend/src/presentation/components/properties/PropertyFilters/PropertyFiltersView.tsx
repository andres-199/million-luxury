import {
  Box,
  TextField,
  Paper,
  Typography,
  Slider,
  IconButton,
  Grid,
} from "@mui/material";
import { Search, Clear } from "@mui/icons-material";
import { formatCurrency } from "../../../utils";
import { useTranslation } from "react-i18next";
import { MAX_PRICE, MIN_PRICE } from "../../../../infrastructure/store";

interface PropertyFiltersViewProps {
  localSearchText: string;
  setLocalSearchText: (text: string) => void;
  priceRange: number[];
  handlePriceChange: (event: Event, newValue: number | number[]) => void;
  handlePriceCommit: (
    event: Event | React.SyntheticEvent,
    newValue: number | number[]
  ) => void;
  handleClearSearch: () => void;
}

export const PropertyFiltersView = ({
  localSearchText,
  setLocalSearchText,
  priceRange,
  handlePriceChange,
  handlePriceCommit,
  handleClearSearch,
}: PropertyFiltersViewProps) => {
  const { t } = useTranslation();

  return (
    <Paper
      elevation={0}
      sx={{
        p: 1.5,
        mb: 0,
        background: (theme) =>
          theme.palette.mode === "light"
            ? "rgba(255, 255, 255, 0.6)"
            : "rgba(21, 27, 36, 0.6)",
        backdropFilter: "blur(10px)",
        border: (theme) =>
          `1px solid ${
            theme.palette.mode === "light"
              ? "rgba(201, 169, 97, 0.2)"
              : "rgba(212, 175, 55, 0.2)"
          }`,
      }}
    >
      <Grid container spacing={2} alignItems="center">
        <Grid size={{ xs: 12, sm: 6, lg: 5 }}>
          <TextField
            fullWidth
            label={t("properties.filters.search.placeholder")}
            variant="outlined"
            size="small"
            value={localSearchText}
            onChange={(e) => setLocalSearchText(e.target.value)}
            slotProps={{
              input: {
                startAdornment: (
                  <Search sx={{ mr: 1, color: "action.active" }} />
                ),
                endAdornment: localSearchText && (
                  <IconButton size="small" onClick={handleClearSearch}>
                    <Clear />
                  </IconButton>
                ),
              },
            }}
          />
        </Grid>

        <Grid size={{ xs: 12, sm: 6, lg: 5 }}>
          <Box sx={{ px: 1 }}>
            <Typography variant="body2" gutterBottom>
              {t("properties.filters.price.title")}:{" "}
              {formatCurrency(priceRange[0])} - {formatCurrency(priceRange[1])}
            </Typography>
            <Slider
              value={priceRange}
              onChange={handlePriceChange}
              onChangeCommitted={handlePriceCommit}
              valueLabelDisplay="auto"
              min={MIN_PRICE}
              max={MAX_PRICE}
              step={50000}
              valueLabelFormat={(value) => formatCurrency(value)}
              size="small"
            />
          </Box>
        </Grid>
      </Grid>
    </Paper>
  );
};
