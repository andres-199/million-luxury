import { Box, ImageList, ImageListItem } from "@mui/material";
import type { PropertyImage } from "../../../../domain/entities";

interface PropertyImagesProps {
  images: PropertyImage[];
  propertyName: string;
}

export const PropertyImages = ({
  images,
  propertyName,
}: PropertyImagesProps) => {
  const enabledImages = images.filter((img) => img.enabled);

  if (enabledImages.length === 0) return null;

  return (
    <Box>
      <ImageList
        cols={3}
        gap={8}
        sx={{
          maxHeight: { xs: "auto", sm: 300 },
          "& .MuiImageListItem-root": {
            height: { xs: 180, sm: 150 } + "!important",
          },
        }}
      >
        {enabledImages.map((image, i) => (
          <ImageListItem key={image.id + i}>
            <img
              src={image.file}
              alt={propertyName}
              loading="lazy"
              style={{
                borderRadius: 8,
                objectFit: "cover",
                height: "100%",
                width: "100%",
              }}
            />
          </ImageListItem>
        ))}
      </ImageList>
    </Box>
  );
};
