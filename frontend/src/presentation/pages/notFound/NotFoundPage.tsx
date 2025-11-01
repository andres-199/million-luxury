import { Box, Typography, Button } from "@mui/material";
import { Home } from "@mui/icons-material";
import { useNavigate } from "react-router-dom";

export const NotFoundPage = () => {
  const navigate = useNavigate();

  return (
    <Box
      display="flex"
      flexDirection="column"
      alignItems="center"
      justifyContent="center"
      minHeight="60vh"
      textAlign="center"
    >
      <Typography variant="h1" color="primary" fontWeight="bold">
        404
      </Typography>
      <Typography variant="h5" gutterBottom>
        Page Not Found
      </Typography>
      <Typography variant="body1" color="text.secondary" mb={3}>
        The page you are looking for doesn't exist.
      </Typography>
      <Button
        variant="contained"
        startIcon={<Home />}
        onClick={() => navigate("/")}
      >
        Go to Home
      </Button>
    </Box>
  );
};
