/** @type {import('next').NextConfig} */
const nextConfig = {}


module.exports = {
  images: {
    remotePatterns: [
      {
        protocol: "http",
        hostname: "localhost",
        port: "",
        pathname: "api/Images/**",
      },
    ],
  },
};