import { configureStore } from "@reduxjs/toolkit";
import { baseApi } from "../api/baseApi";
import { filterReducer, themeReducer } from "./slices";

export const store = configureStore({
  reducer: {
    [baseApi.reducerPath]: baseApi.reducer,
    filter: filterReducer,
    theme: themeReducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(baseApi.middleware),
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
