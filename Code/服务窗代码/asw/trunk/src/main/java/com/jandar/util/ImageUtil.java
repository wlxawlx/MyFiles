package com.jandar.util;

import javax.imageio.ImageIO;
import java.awt.*;
import java.awt.geom.AffineTransform;
import java.awt.image.AffineTransformOp;
import java.awt.image.BufferedImage;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.IOException;
import java.util.Arrays;

/**
 * 生成图片的缩略图
 * Created by zzw on 16/6/14.
 */
public class ImageUtil {

    private static String DEFAULT_PREVFIX = "thumb_";
    private static Boolean DEFAULT_FORCE = false;

    /**
     * <p>Title: thumbnailImage</p>
     * <p>Description: 根据图片路径生成缩略图 </p>
     *
     * @param imgFile 原图片路径
     * @param w       缩略图宽
     * @param h       缩略图高
     * @param force   是否强制按照宽高生成缩略图(如果为false，则生成最佳比例缩略图)
     */
    private static BufferedImage thumbnailImage(File imgFile, int w, int h, boolean force) throws Exception {
        if (!imgFile.exists()) {
            throw new Exception("the image is not exist.");
        }

        // ImageIO 支持的图片类型 : [BMP, bmp, jpg, JPG, wbmp, jpeg, png, PNG, JPEG, WBMP, GIF, gif]
        final String types = Arrays.toString(ImageIO.getReaderFormatNames());
        // 获取图片后缀
        String suffix = FileUtil.getFileSuffix(imgFile.getName());

        // 类型和图片后缀全部小写，然后判断后缀是否合法
        if (suffix == null || !types.toLowerCase().contains(suffix.toLowerCase())) {
            throw new Exception("Sorry, the image suffix is illegal. the standard image suffix is {}." + types);
        }
        Image img = ImageIO.read(imgFile);
        if (!force) {
            // 根据原图与要求的缩略图比例，找到最合适的缩略图比例
            int width = img.getWidth(null);
            int height = img.getHeight(null);
            if ((width * 1.0) / w < (height * 1.0) / h) {
                if (width > w) {
                    h = Integer.parseInt(new java.text.DecimalFormat("0").format(height * w / (width * 1.0)));
                }
            } else {
                if (height > h) {
                    w = Integer.parseInt(new java.text.DecimalFormat("0").format(width * h / (height * 1.0)));
                }
            }
        }
        // 重绘
        BufferedImage bi = new BufferedImage(w, h, BufferedImage.TYPE_INT_RGB);
        Graphics g = bi.getGraphics();
        g.drawImage(img, 0, 0, w, h, Color.LIGHT_GRAY, null);
        g.dispose();

        return bi;
    }

    public static BufferedImage resizeImage(final BufferedImage bufferedimage, final int width, final int height) {
        double Ratio = 0.0;
        java.awt.Image itemp = bufferedimage.getScaledInstance(width, height, BufferedImage.SCALE_SMOOTH);
        if ((bufferedimage.getHeight() > width) || (bufferedimage.getWidth() > height)) {
            if (bufferedimage.getHeight() > bufferedimage.getWidth())
                Ratio = (double) width / bufferedimage.getHeight();
            else
                Ratio = (double) height / bufferedimage.getWidth();
        }
        AffineTransformOp op = new AffineTransformOp(AffineTransform.getScaleInstance(Ratio, Ratio), null);
        itemp = op.filter(bufferedimage, null);
        return (BufferedImage) itemp;
    }

    private void saveToFile(BufferedImage bi, File imgFile, String prevfix) throws IOException {
        String p = imgFile.getPath();
        String filepath = p.substring(0, p.lastIndexOf(File.separator)) + File.separator + prevfix + imgFile.getName();
        String suffix = FileUtil.getFileSuffix(imgFile.getName());
        // 将图片保存在原目录并加上前缀
        ImageIO.write(bi, suffix, new File(filepath));
    }

    public void thumbnailImage(String imagePath, int w, int h, String prevfix, boolean force) throws Exception {
        File imgFile = new File(imagePath);
        BufferedImage bufferedImage = thumbnailImage(imgFile, w, h, force);
        saveToFile(bufferedImage, imgFile, prevfix);
    }

    public void thumbnailImage(String imagePath, int w, int h, boolean force) throws Exception {
        thumbnailImage(imagePath, w, h, DEFAULT_PREVFIX, force);
    }

    public void thumbnailImage(String imagePath, int w, int h) throws Exception {
        thumbnailImage(imagePath, w, h, DEFAULT_FORCE);
    }

    public static byte[] bufferedImage2ByteArray(BufferedImage bufferedImage) throws IOException {
        ByteArrayOutputStream baos = new ByteArrayOutputStream();
        ImageIO.write(bufferedImage, "jpg", baos);
        baos.flush();
        byte[] imageInByte = baos.toByteArray();
        baos.close();
        return imageInByte;
    }

    public static byte[] thumbnailImageByte(String imagePath, int w, int h) throws Exception {
        File imgFile = new File(imagePath);
        BufferedImage bufferedImage = thumbnailImage(imgFile, w, h, DEFAULT_FORCE);

        return bufferedImage2ByteArray(bufferedImage);
    }

}
