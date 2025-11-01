import { Card, CardContent, Skeleton } from "@mui/material";

export const PropertyCardSkeleton = () => {
  return (
    <Card
      sx={{
        height: "100%",
        width: "100%",
        display: "flex",
        flexDirection: "column",
        background: (theme) =>
          theme.palette.mode === "light"
            ? "rgba(255, 255, 255, 0.7)"
            : "rgba(21, 27, 36, 0.7)",
        backdropFilter: "blur(10px)",
        border: (theme) =>
          `1px solid ${
            theme.palette.mode === "light"
              ? "rgba(201, 169, 97, 0.3)"
              : "rgba(212, 175, 55, 0.3)"
          }`,
      }}
    >
      <Skeleton variant="rectangular" height={400} sx={{ flexGrow: 1 }} />
      <CardContent sx={{ py: 2 }}>
        <Skeleton variant="text" sx={{ fontSize: "1.5rem", mb: 0.5 }} />
        <Skeleton variant="text" sx={{ mb: 1 }} />
        <Skeleton variant="text" width="60%" />
      </CardContent>
    </Card>
  );
};
