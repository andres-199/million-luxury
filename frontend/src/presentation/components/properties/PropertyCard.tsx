import { Card, CardContent, Typography, Box } from "@mui/material";
import { LocationOn } from "@mui/icons-material";
import type { Property } from "../../../domain/entities";
import { formatCurrency } from "../../utils";
import { ImageCarousel } from "../common/ImageCarousel";

interface PropertyCardProps {
  property: Property;
  onClick: () => void;
}

export const PropertyCard = ({ property, onClick }: PropertyCardProps) => {
  const hasEnabledImages = property.images.some((img) => img.enabled);

  return (
    <Card
      sx={{
        cursor: "pointer",
        height: "100%",
        width: "100%",
        display: "flex",
        flexDirection: "column",
        minWidth: 0,
        maxWidth: "100%",
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
        transition:
          "transform 0.3s ease, box-shadow 0.3s ease, background 0.3s ease",
        "&:hover": {
          transform: "translateY(-8px)",
          boxShadow: 8,
          background: (theme) =>
            theme.palette.mode === "light"
              ? "rgba(255, 255, 255, 0.9)"
              : "rgba(21, 27, 36, 0.9)",
          "& .year-sticker": {
            transform: "rotate(8deg) translateY(-2px)",
          },
        },
        position: "relative",
      }}
      onClick={onClick}
    >
      <Box
        className="year-sticker"
        sx={{
          position: "absolute",
          top: 16,
          right: 16,
          zIndex: 1,
          background: (theme) => theme.palette.primary.main,
          color: "white",
          padding: "6px 12px",
          borderRadius: "4px",
          fontWeight: 600,
          transform: "rotate(4deg)",
          transition: "transform 0.3s ease",
          boxShadow: "2px 2px 6px rgba(0,0,0,0.2)",
          "&::before": {
            content: '""',
            position: "absolute",
            top: 0,
            left: 0,
            right: 0,
            bottom: 0,
            background: "rgba(255,255,255,0.1)",
            borderRadius: "inherit",
          },
        }}
      >
        {property.year}
      </Box>
      <ImageCarousel
        images={property.images}
        name={property.name}
        height={300}
        showControls={property.images.filter((img) => img.enabled).length > 1}
      />
      <CardContent sx={{ py: 2, px: 2.5 }}>
        <Box display="flex" justifyContent="space-between" gap={2}>
          <Box flex={1} width="60%">
            <Typography
              variant="h6"
              component="h2"
              noWrap
              sx={{ mb: 0.5, fontWeight: 700 }}
            >
              {property.name}
            </Typography>

            <Box display="flex" alignItems="center" gap={0.5}>
              <LocationOn fontSize="small" sx={{ color: "primary.main" }} />
              <Typography
                variant="body2"
                color="text.secondary"
                noWrap
                sx={{ flex: 1 }}
              >
                {property.address}
              </Typography>
            </Box>
          </Box>

          <Box
            display="flex"
            flexDirection="column"
            alignItems="flex-end"
            justifyContent="flex-end"
          >
            <Typography variant="h6" color="primary" fontWeight="bold" noWrap>
              {formatCurrency(property.price)}
            </Typography>
          </Box>
        </Box>
      </CardContent>
    </Card>
  );
};
