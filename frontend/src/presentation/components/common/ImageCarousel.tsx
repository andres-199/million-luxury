import { Box, IconButton, MobileStepper } from "@mui/material";
import { KeyboardArrowLeft, KeyboardArrowRight } from "@mui/icons-material";
import { useState } from "react";
import type { PropertyImage } from "../../../domain/entities";

interface ImageCarouselProps {
  images: PropertyImage[];
  name: string;
  height?: number | { xs?: number; sm?: number };
  showControls?: boolean;
}

export const ImageCarousel = ({
  images,
  name,
  height = { xs: 300, sm: 400 },
  showControls = true,
}: ImageCarouselProps) => {
  const [activeStep, setActiveStep] = useState(0);
  const enabledImages = images.filter((img) => img.enabled);
  const maxSteps = enabledImages.length;

  if (enabledImages.length === 0) return null;

  const handleNext = (e: React.MouseEvent<HTMLButtonElement>) => {
    e.stopPropagation();
    setActiveStep((prevActiveStep) => prevActiveStep + 1);
  };

  const handleBack = (e: React.MouseEvent<HTMLButtonElement>) => {
    e.stopPropagation();
    setActiveStep((prevActiveStep) => prevActiveStep - 1);
  };

  return (
    <Box sx={{ position: "relative" }}>
      <Box
        sx={{
          height,
          width: "100%",
          position: "relative",
          overflow: "hidden",
          borderRadius: 1,
        }}
      >
        {enabledImages.map((image, index) => (
          <Box
            key={image.id}
            sx={{
              position: "absolute",
              width: "100%",
              height: "100%",
              opacity: index === activeStep ? 1 : 0,
              transition: "opacity 500ms ease-in-out",
            }}
          >
            {Math.abs(activeStep - index) <= 2 ? (
              <img
                src={image.file}
                alt={`${name} - ${index + 1}`}
                style={{
                  width: "100%",
                  height: "100%",
                  objectFit: "cover",
                }}
                loading={index === 0 ? "eager" : "lazy"}
              />
            ) : null}
          </Box>
        ))}
      </Box>

      {showControls && maxSteps > 1 && (
        <>
          <IconButton
            size="large"
            onClick={handleBack}
            disabled={activeStep === 0}
            sx={{
              position: "absolute",
              left: 8,
              top: "50%",
              transform: "translateY(-50%)",
              bgcolor: "rgba(255, 255, 255, 0.4)",
              "&:hover": {
                bgcolor: "rgba(255, 255, 255, 0.7)",
              },
            }}
          >
            <KeyboardArrowLeft />
          </IconButton>

          <IconButton
            size="large"
            onClick={handleNext}
            disabled={activeStep === maxSteps - 1}
            sx={{
              position: "absolute",
              right: 8,
              top: "50%",
              transform: "translateY(-50%)",
              bgcolor: "rgba(255, 255, 255, 0.4)",
              "&:hover": {
                bgcolor: "rgba(255, 255, 255, 0.7)",
              },
            }}
          >
            <KeyboardArrowRight />
          </IconButton>

          <MobileStepper
            steps={maxSteps}
            position="static"
            activeStep={activeStep}
            sx={{
              position: "absolute",
              bottom: 0,
              width: "100%",
              bgcolor: "transparent",
              "& .MuiMobileStepper-dot": {
                bgcolor: "rgba(255, 255, 255, 0.5)",
              },
              "& .MuiMobileStepper-dotActive": {
                bgcolor: "primary.main",
              },
            }}
            nextButton={<Box />}
            backButton={<Box />}
          />
        </>
      )}
    </Box>
  );
};
