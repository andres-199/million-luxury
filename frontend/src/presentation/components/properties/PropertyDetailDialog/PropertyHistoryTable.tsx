import {
  TableContainer,
  Paper,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
} from "@mui/material";
import type { PropertyTrace } from "../../../../domain/entities";
import { formatCurrency, formatShortDate } from "../../../utils";
import { useTranslation } from "react-i18next";

interface PropertyHistoryTableProps {
  traces: PropertyTrace[];
}

export const PropertyHistoryTable = ({ traces }: PropertyHistoryTableProps) => {
  const { t } = useTranslation();

  return (
    <TableContainer
      component={Paper}
      variant="outlined"
      sx={{
        maxWidth: "100%",
        overflowX: "auto",
      }}
    >
      <Table
        size="small"
        sx={{
          minWidth: { xs: 400, sm: "auto" },
        }}
      >
        <TableHead>
          <TableRow>
            <TableCell>
              <strong>{t("properties.history.table.date")}</strong>
            </TableCell>
            <TableCell>
              <strong>{t("properties.history.table.name")}</strong>
            </TableCell>
            <TableCell align="right">
              <strong>{t("properties.history.table.value")}</strong>
            </TableCell>
            <TableCell align="right">
              <strong>{t("properties.history.table.tax")}</strong>
            </TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {traces.map((trace) => (
            <TableRow key={trace.id}>
              <TableCell>{formatShortDate(trace.dateSale)}</TableCell>
              <TableCell
                sx={{
                  maxWidth: { xs: 120, sm: "none" },
                  whiteSpace: "nowrap",
                  overflow: "hidden",
                  textOverflow: "ellipsis",
                }}
              >
                {trace.name}
              </TableCell>
              <TableCell align="right">{formatCurrency(trace.value)}</TableCell>
              <TableCell align="right">{formatCurrency(trace.tax)}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};
