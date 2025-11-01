import { ThemeProvider } from "@mui/material/styles";
import { CssBaseline } from "@mui/material";
import { Provider } from "react-redux";
import { store, useAppSelector } from "./infrastructure/store";
import { getTheme } from "./presentation/theme";
import { AppRouter } from "./presentation/router";

function AppContent() {
  const themeMode = useAppSelector((state) => state.theme.mode);
  const theme = getTheme(themeMode);

  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <AppRouter />
    </ThemeProvider>
  );
}

function App() {
  return (
    <Provider store={store}>
      <AppContent />
    </Provider>
  );
}

export default App;
