import { Box, Typography, Divider } from "@mui/material";
import type { PropertyTrace } from "../../../../domain/entities";
import { useTranslation } from "react-i18next";
import { PropertyHistoryTable } from "./PropertyHistoryTable";

interface PropertyHistoryProps {
  traces: PropertyTrace[];
}

export const PropertyHistory = ({ traces }: PropertyHistoryProps) => {
  const { t } = useTranslation();

  if (!traces || traces.length === 0) return null;

  return (
    <Box>
      <Divider sx={{ my: 2 }} />
      <Typography variant="h6" gutterBottom>
        {t("properties.history.title")}
      </Typography>
      <PropertyHistoryTable traces={traces} />
    </Box>
  );
};
