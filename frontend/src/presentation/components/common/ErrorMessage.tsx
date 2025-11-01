import { Alert, AlertTitle, Box } from "@mui/material";
import { useTranslation } from "react-i18next";

interface ErrorMessageProps {
  message?: string;
  title?: string;
}

export const ErrorMessage = ({
  message,
  title = "Error",
}: ErrorMessageProps) => {
  const { t } = useTranslation();
  return (
    <Box sx={{ p: 2 }}>
      <Alert severity="error">
        <AlertTitle>{title}</AlertTitle>
        {message ||
          t("common.error.generic") + ". " + t("common.error.tryAgain")}
      </Alert>
    </Box>
  );
};
