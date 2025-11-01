import { createTheme } from "@mui/material/styles";
import type { ThemeMode } from "../../infrastructure/store";

export const getTheme = (mode: ThemeMode) =>
  createTheme({
    palette: {
      mode,
      primary: {
        main: mode === "light" ? "#C9A961" : "#D4AF37",
        light: mode === "light" ? "#E5D4A1" : "#F4E5B8",
        dark: mode === "light" ? "#A08443" : "#B8963E",
      },
      secondary: {
        main: mode === "light" ? "#2C3E50" : "#5D6D7E",
        light: mode === "light" ? "#566573" : "#85929E",
        dark: mode === "light" ? "#1C2833" : "#34495E",
      },
      background: {
        default: mode === "light" ? "#FAFAFA" : "#0A0E14",
        paper: mode === "light" ? "#FFFFFF" : "#151B24",
      },
      text: {
        primary: mode === "light" ? "#1C2833" : "#ECF0F1",
        secondary: mode === "light" ? "#566573" : "#AEB6BF",
      },
    },
    typography: {
      fontFamily: '"Playfair Display", "Georgia", "Times New Roman", serif',
      h1: {
        fontWeight: 700,
        letterSpacing: "-0.02em",
      },
      h2: {
        fontWeight: 700,
        letterSpacing: "-0.01em",
      },
      h3: {
        fontWeight: 700,
      },
      h4: {
        fontWeight: 700,
        letterSpacing: "-0.01em",
      },
      h5: {
        fontWeight: 600,
      },
      h6: {
        fontWeight: 600,
      },
      body1: {
        fontFamily: '"Lato", "Roboto", "Helvetica", "Arial", sans-serif',
      },
      body2: {
        fontFamily: '"Lato", "Roboto", "Helvetica", "Arial", sans-serif',
      },
      button: {
        fontFamily: '"Lato", "Roboto", "Helvetica", "Arial", sans-serif',
        fontWeight: 600,
        letterSpacing: "0.5px",
      },
    },
    components: {
      MuiCard: {
        styleOverrides: {
          root: {
            borderRadius: 12,
            boxShadow:
              mode === "light"
                ? "0 4px 20px rgba(0,0,0,0.08)"
                : "0 4px 20px rgba(0,0,0,0.4)",
            transition:
              "transform 0.2s ease-in-out, box-shadow 0.2s ease-in-out",
            "&:hover": {
              boxShadow:
                mode === "light"
                  ? "0 8px 30px rgba(0,0,0,0.12)"
                  : "0 8px 30px rgba(0,0,0,0.6)",
            },
          },
        },
      },
      MuiButton: {
        styleOverrides: {
          root: {
            borderRadius: 8,
            textTransform: "none",
            fontWeight: 600,
            padding: "10px 24px",
          },
        },
      },
      MuiPaper: {
        styleOverrides: {
          root: {
            borderRadius: 12,
          },
        },
      },
    },
  });
