import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Layout } from "../components/layout";
import { HomePage, NotFoundPage } from "../pages";

export const AppRouter = () => {
  return (
    <BrowserRouter>
      <Layout>
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="*" element={<NotFoundPage />} />
        </Routes>
      </Layout>
    </BrowserRouter>
  );
};
