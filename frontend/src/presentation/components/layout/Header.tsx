import { AppBar, Toolbar, Typography, IconButton, Box } from "@mui/material";
import { Brightness4, Brightness7 } from "@mui/icons-material";
import {
  useAppDispatch,
  useAppSelector,
  toggleTheme,
} from "../../../infrastructure/store";

export const Header = () => {
  const dispatch = useAppDispatch();
  const themeMode = useAppSelector((state) => state.theme.mode);

  const handleToggleTheme = () => {
    dispatch(toggleTheme());
  };

  return (
    <AppBar
      position="static"
      elevation={0}
      sx={{
        background: (theme) =>
          theme.palette.mode === "light"
            ? "linear-gradient(135deg, #2C3E50 0%, #34495E 100%)"
            : "linear-gradient(135deg, #0A0E14 0%, #151B24 100%)",
        borderBottom: (theme) => `2px solid ${theme.palette.primary.main}`,
        borderRadius: 0,
      }}
    >
      <Toolbar sx={{ py: 1 }}>
        <Typography
          variant="h5"
          component="div"
          sx={{
            flexGrow: 1,
            fontWeight: 700,
            letterSpacing: "1px",
            background: (theme) =>
              `linear-gradient(135deg, ${theme.palette.primary.main} 0%, ${theme.palette.primary.light} 100%)`,
            WebkitBackgroundClip: "text",
            WebkitTextFillColor: "transparent",
            backgroundClip: "text",
          }}
        >
          Million Luxury
        </Typography>
        <Box>
          <IconButton
            onClick={handleToggleTheme}
            color="inherit"
            sx={{
              color: (theme) => theme.palette.primary.main,
              "&:hover": {
                background: (theme) =>
                  theme.palette.mode === "light"
                    ? "rgba(201, 169, 97, 0.1)"
                    : "rgba(212, 175, 55, 0.1)",
              },
            }}
          >
            {themeMode === "dark" ? <Brightness7 /> : <Brightness4 />}
          </IconButton>
        </Box>
      </Toolbar>
    </AppBar>
  );
};
