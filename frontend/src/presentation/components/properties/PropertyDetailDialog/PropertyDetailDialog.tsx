import {
  Dialog,
  DialogTitle,
  DialogContent,
  Box,
  Typography,
  Stack,
  IconButton,
} from "@mui/material";
import { Close } from "@mui/icons-material";
import type { Property } from "../../../../domain/entities";
import { PropertyImages } from "./PropertyImages";
import { PropertyBasicInfo } from "./PropertyBasicInfo";
import { PropertyHistory } from "./PropertyHistory";

interface PropertyDetailDialogProps {
  property: Property | null;
  open: boolean;
  onClose: () => void;
}

export const PropertyDetailDialog = ({
  property,
  open,
  onClose,
}: PropertyDetailDialogProps) => {
  if (!property) return null;

  return (
    <Dialog
      open={open}
      onClose={onClose}
      maxWidth="md"
      fullWidth
      fullScreen={false}
      sx={{
        "& .MuiDialog-paper": {
          width: "100%",
          margin: { xs: 0, sm: 2 },
          maxHeight: { xs: "100%", sm: "calc(100% - 64px)" },
          height: { xs: "100%", sm: "auto" },
          borderRadius: { xs: 0, sm: 2 },
        },
      }}
    >
      <DialogTitle sx={{ px: { xs: 2, sm: 3 }, py: { xs: 2, sm: 2.5 } }}>
        <Box display="flex" justifyContent="space-between" alignItems="center">
          <Typography variant="h5" component="span" sx={{ pr: 3 }}>
            {property.name}
          </Typography>
          <IconButton
            onClick={onClose}
            sx={{
              position: { xs: "absolute", sm: "static" },
              right: 8,
              top: 8,
            }}
          >
            <Close />
          </IconButton>
        </Box>
      </DialogTitle>

      <DialogContent
        dividers
        sx={{ px: { xs: 2, sm: 3 }, py: { xs: 2, sm: 3 } }}
      >
        <Stack spacing={3}>
          <PropertyImages
            images={property.images}
            propertyName={property.name}
          />
          <PropertyBasicInfo property={property} />
          <PropertyHistory traces={property.traces} />
        </Stack>
      </DialogContent>
    </Dialog>
  );
};
