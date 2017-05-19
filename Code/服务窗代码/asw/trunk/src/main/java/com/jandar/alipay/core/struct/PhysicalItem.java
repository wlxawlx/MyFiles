package com.jandar.alipay.core.struct;

/*
 * 体检项目
 */
public class PhysicalItem {


	public PhysicalItem() {
		super();
	}
	
	private String itemCategory;//项目分类  医生检查或不同科室（检验科，功能科等）
	private String itemName;// 项目名称
	public String getItemCategory() {
		return itemCategory;
	}
	public void setItemCategory(String itemCategory) {
		this.itemCategory = itemCategory;
	}
	public String getItemName() {
		return itemName;
	}
	public void setItemName(String itemName) {
		this.itemName = itemName;
	}
	
	
	
	
}
