   M S B A M L   `   `   `  ����X  TXceed.Wpf.Toolkit, Version=4.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4P:clr-namespace:Xceed.Wpf.Toolkit;assembly=Xceed.Wpf.ToolkitXceed.Wpf.Toolkit  5   	   B >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ProjectProject 5   	      Project.AddTextWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Ocontrols:clr-namespace:Xceed.Wpf.Toolkit;assembly=Xceed.Wpf.Toolkit     5   	   'localclr-namespace:Project     5   	     � Title$  Add Text��5	   	   $	��215��6   $	��420��6'   
�������.��5      �  � RowDefinitions 5   
   �� 5      $��15 �6   �� 5      $��35 �6   �� 5      $��35 �6   �� 5      $��35 �6   �� 5      $��* �6   �� 5      $��40 �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��Input Text:��6   $��1��6&   $��Center=�5      $��Center=�6+   �� 5   
   -   5         ��Name$ textBox��5      $��1��6#   $��1��63   $��Center=�5      $��Stretch=�6-   $��5 0 0 0q�5      $��Center=�6$   $��18(�6F   $��35��5       $	��210��6    �� TextWrapping$
 Wrap=�5!      �� 5$   
   $��Text Color:��6   $��2��6&   $��Center=�5%      $��Center=�6+   #   Xceed.Wpf.Toolkit.ColorPicker  5'   
   -   5          Name$ 	textColor��5+      $��2��5'      $��Center=�5(      $��Center=�6:   $��35��5)      $	��210��6+   $��1��5*      $��2��5+   2   �� 5.   
   $��
Text Size:��6   $��3��6%   $��Center=�5/      $��Center=�6+   �� 51   
   -   5        $ textSize��51      $��1��6$   $��3��64   $��Center=�52      $��Stretch=�6-   $��5 0 0 0q�53      $��Center=�6$   $��18(�6F   $��35��54      $	��210��6   $
 Wrap=�55      � 58   
   $��2��6   $��5��6#    59      �� 5:      $��* �6#   �� 5;      $��* �6#   .+�5?      �� -   5         ��Name$ addTextButton��5C      $	��Add��5?      $��0��5@      
�������5C      �� 5F      -   5        $��Close��5F      $��1��5G      
�������5I      5J      5K      5L      