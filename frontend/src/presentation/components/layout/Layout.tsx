import { Container, Box } from "@mui/material";
import { Header } from "./Header";
import type { ReactNode } from "react";

interface LayoutProps {
  children: ReactNode;
}

export const Layout = ({ children }: LayoutProps) => {
  return (
    <Box
      sx={{
        display: "flex",
        flexDirection: "column",
        minHeight: "100vh",
        minWidth: "100vw",
      }}
    >
      <Header />
      <Container 
        maxWidth="xl" 
        sx={{ 
          mt: 4, 
          mb: 4, 
          flex: 1,
          width: '100%',
          px: { xs: 2, sm: 3 }
        }}
      >
        {children}
      </Container>
    </Box>
  );
};
