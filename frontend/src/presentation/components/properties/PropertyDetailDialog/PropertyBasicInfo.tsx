import { Box, Stack, Typography, Chip } from "@mui/material";
import { LocationOn, Tag, CalendarToday } from "@mui/icons-material";
import type { Property } from "../../../../domain/entities";
import { formatCurrency } from "../../../utils";
import { useTranslation } from "react-i18next";

interface PropertyBasicInfoProps {
  property: Property;
}

export const PropertyBasicInfo = ({ property }: PropertyBasicInfoProps) => {
  const { t } = useTranslation();

  return (
    <Stack spacing={2}>
      <Box display="flex" alignItems="center" gap={1}>
        <LocationOn color="action" />
        <Typography variant="body1">{property.address}</Typography>
      </Box>

      <Box display="flex" alignItems="center" gap={1}>
        <Tag color="action" />
        <Typography variant="body1">
          {t("properties.details.code")}:{" "}
          <strong>{property.codeInternal}</strong>
        </Typography>
      </Box>

      <Box display="flex" alignItems="center" gap={1}>
        <CalendarToday color="action" />
        <Typography variant="body1">
          {t("properties.details.year")}:{" "}
          <Chip label={property.year} size="small" color="primary" />
        </Typography>
      </Box>

      <Box>
        <Typography variant="h4" color="primary" fontWeight="bold">
          {formatCurrency(property.price)}
        </Typography>
      </Box>
    </Stack>
  );
};
