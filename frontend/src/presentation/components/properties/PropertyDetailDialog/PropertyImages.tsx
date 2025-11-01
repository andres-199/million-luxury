import type { PropertyImage } from "../../../../domain/entities";
import { ImageCarousel } from "../../common/ImageCarousel";

interface PropertyImagesProps {
  images: PropertyImage[];
  propertyName: string;
}

export const PropertyImages = ({
  images,
  propertyName,
}: PropertyImagesProps) => {
  return (
    <ImageCarousel
      images={images}
      name={propertyName}
      height={{ xs: 300, sm: 400 }}
    />
  );
};
